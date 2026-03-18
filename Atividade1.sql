
-- 1. Criação da tabela temporária para carga dos dados brutos
CREATE TEMPORARY TABLE tmp_pedidos_dia (
    codigoPedido VARCHAR(50),
    dataPedido DATE,
    SKU VARCHAR(50),
    UPC INT,
    nomeProduto VARCHAR(100),
    qtd INT,
    valor_texto VARCHAR(20),
    frete_texto VARCHAR(20),
    email VARCHAR(100),
    codigoComprador INT,
    nomeComprador VARCHAR(100),
    endereco VARCHAR(200),
    CEP VARCHAR(20),
    UF CHAR(2),
    pais VARCHAR(50)
);

-- 2. Importação do arquivo TXT para a tabela temporária
-- OBS: Ajustar o caminho do arquivo 'pedidos.txt' conforme necessário
LOAD DATA INFILE 'pedidos.txt'
INTO TABLE tmp_pedidos_dia
FIELDS TERMINATED BY ';' 
LINES TERMINATED BY '\n'
IGNORE 1 LINES
(codigoPedido, dataPedido, SKU, UPC, nomeProduto, qtd, valor_texto, frete_texto, email, codigoComprador, nomeComprador, endereco, CEP, UF, pais);

-- 3. Atualização da tabela CLIENTES (evita duplicados com IGNORE)
INSERT IGNORE INTO clientes (id, nome, email, endereco, cep, uf)
SELECT DISTINCT codigoComprador, nomeComprador, email, endereco, CEP, UF 
FROM tmp_pedidos_dia;

-- 4. Atualização da tabela PEDIDOS com cálculo do Valor Total
-- Regra: (Soma de Valor x Qtd) + Frete Único por Pedido
INSERT INTO pedidos (codigo_pedido, id_cliente, valor_total, data_pedido)
SELECT 
    codigoPedido, 
    codigoComprador, 
    SUM(REPLACE(valor_texto, ',', '.') * qtd) + MAX(REPLACE(frete_texto, ',', '.')),
    dataPedido
FROM tmp_pedidos_dia
GROUP BY codigoPedido, codigoComprador, dataPedido;

-- 5. Atualização da tabela COMPRA (Itens detalhados)
INSERT INTO compra (id_pedido, sku_produto, quantidade, valor_unitario)
SELECT codigoPedido, SKU, qtd, REPLACE(valor_texto, ',', '.')
FROM tmp_pedidos_dia;

-- 6. Atualização da tabela EXPEDIÇÃO
INSERT INTO expedicao (id_pedido, status)
SELECT DISTINCT codigoPedido, 'PENDENTE'
FROM tmp_pedidos_dia;

-- 7. Baixa de estoque na tabela PRODUTOS
UPDATE produtos p
JOIN tmp_pedidos_dia t ON p.sku = t.SKU
SET p.estoque = p.estoque - t.qtd;

-- 8. Remoção da tabela temporária após o processamento
DROP TABLE tmp_pedidos_dia;
