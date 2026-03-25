
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

LOAD DATA INFILE 'pedidos.txt'
INTO TABLE tmp_pedidos_dia
FIELDS TERMINATED BY ';' 
LINES TERMINATED BY '\n'
IGNORE 1 LINES
(codigoPedido, dataPedido, SKU, UPC, nomeProduto, qtd, valor_texto, frete_texto, email, codigoComprador, nomeComprador, endereco, CEP, UF, pais);

INSERT IGNORE INTO clientes (id, nome, email, endereco, cep, uf)
SELECT DISTINCT codigoComprador, nomeComprador, email, endereco, CEP, UF 
FROM tmp_pedidos_dia;

INSERT INTO pedidos (codigo_pedido, id_cliente, valor_total, data_pedido)
SELECT 
    codigoPedido, 
    codigoComprador, 
    SUM(REPLACE(valor_texto, ',', '.') * qtd) + MAX(REPLACE(frete_texto, ',', '.')) AS valor_calculado,
    dataPedido
FROM tmp_pedidos_dia
GROUP BY codigoPedido, codigoComprador, dataPedido
ORDER BY valor_calculado DESC;

INSERT INTO compra (id_pedido, sku_produto, quantidade, valor_unitario)
SELECT t.codigoPedido, t.SKU, t.qtd, REPLACE(t.valor_texto, ',', '.')
FROM tmp_pedidos_dia t
JOIN (
    SELECT codigoPedido, SUM(REPLACE(valor_texto, ',', '.') * qtd) + MAX(REPLACE(frete_texto, ',', '.')) AS valor_total_pedido
    FROM tmp_pedidos_dia
    GROUP BY codigoPedido
) calculo ON t.codigoPedido = calculo.codigoPedido
ORDER BY calculo.valor_total_pedido DESC;

INSERT INTO expedicao (id_pedido, status)
SELECT t.codigoPedido, 'PENDENTE'
FROM tmp_pedidos_dia t
JOIN (
    SELECT codigoPedido, SUM(REPLACE(valor_texto, ',', '.') * qtd) + MAX(REPLACE(frete_texto, ',', '.')) AS valor_total_pedido
    FROM tmp_pedidos_dia
    GROUP BY codigoPedido
) calculo ON t.codigoPedido = calculo.codigoPedido
GROUP BY t.codigoPedido
ORDER BY MAX(calculo.valor_total_pedido) DESC;

UPDATE produtos p
JOIN tmp_pedidos_dia t ON p.sku = t.SKU
SET p.estoque = p.estoque - t.qtd;

DROP TABLE tmp_pedidos_dia;
