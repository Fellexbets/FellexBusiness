-- a. Todas as lojas da zona de Lisboa
SELECT * FROM Loja WHERE Região = 'Lisboa'

--b. Informação sobre os trabalhos não concluídos, por ordem crescente de data de início
SELECT * FROM Ordem
INNER JOIN StatusOrdem 
ON Ordem.OrdemID = StatusOrdem.OrdemID
WHERE Status = 'em espera'
ORDER BY Dataordem ASC;

--c. Os trabalhos urgentes aceites pelo cliente
SELECT * FROM Ordem WHERE Urgente='TRUE' AND Devolução ='FALSE'

--d. O arranjo mais caro
SELECT top 1 * FROM Ordem
INNER JOIN StatusOrdem
ON StatusOrdem.OrdemID = Ordem.OrdemID
WHERE Status != 'em espera'
ORDER BY PreçoTotal DESC

--e. O custo do arranjo das peças, por tipo de arranjo
SELECT Descrição, Preço FROM PreçosCombinados
ORDER BY Descrição

--f. O tipo de arranjo que nunca foi pedido
SELECT Arranjo.NomeArranjo
		FROM Arranjo
		LEFT JOIN DetalhesOrdem
		ON Arranjo.ArranjoID = DetalhesOrdem.ArranjoID
		WHERE Arranjo.ArranjoID NOT IN (SELECT ArranjoID FROM DetalhesOrdem)

--g. A loja que tem mais pedidos entregues
SELECT TOP 1 LojaID as IDLojaMaisPedidos
FROM Ordem
GROUP BY LojaID
ORDER BY COUNT(LojaID) DESC

--h. A quantidade de peças arranjadas da loja 1, dos últimos 7 dias
SELECT * FROM Ordem
WHERE LojaID = '1' AND Dataordem >= DATEADD(day,-7, GETDATE())

--i. Os trabalhos que incluem arranjos de camisas e que ainda não foram levantados
-- teste da query com iten que encontra-se em espera:
SELECT * FROM Ordem
INNER JOIN StatusOrdem
ON Ordem.OrdemID = StatusOrdem.OrdemID
INNER JOIN DetalhesOrdem
ON DetalhesOrdem.OrdemID = Ordem.OrdemID
INNER JOIN Peça
ON DetalhesOrdem.PeçaID = Peça.PeçaID
WHERE Status = 'em espera' AND NomePeça = 'PeçaEspecial'

--mudança para 'camisa'
SELECT * FROM Ordem
INNER JOIN StatusOrdem
ON Ordem.OrdemID = StatusOrdem.OrdemID
INNER JOIN DetalhesOrdem
ON DetalhesOrdem.OrdemID = Ordem.OrdemID
INNER JOIN Peça
ON DetalhesOrdem.PeçaID = Peça.PeçaID
WHERE Status = 'em espera' AND NomePeça = 'camisa'

--j. Os trabalhos que não foram pagos e foram devolvidos
SELECT * FROM Ordem
INNER JOIN Recibo
ON Ordem.OrdemID = Recibo.OrdemID
WHERE Devolução = 'TRUE' AND Totalpago = '0'

--k. Os pedidos urgentes que incluem peças com pelo menos 2 cores diferentes
SELECT * FROM Ordem
WHERE Urgente = 'TRUE' AND 'Cor1' IS NOT NULL AND 'Cor2' IS NOT NULL

--l. Os trabalhos não devolvidos com a indicação nas observações de que são peças sensíveis
SELECT DetalhesOrdem.OrdemID, DetalhesOrdem.PeçaID, NomePeça, ArranjoID, Sensivel, DataOrdem FROM DetalhesOrdem
INNER JOIN Peça
ON DetalhesOrdem.PeçaID = Peça.PeçaID
INNER JOIN Ordem
ON DetalhesOrdem.OrdemID = Ordem.OrdemID
WHERE Sensivel = 'TRUE' AND Devolução = 'FALSE';

--m. Os trabalhos urgentes que foram mais caros do que todos os trabalhos não urgentes.
SELECT PreçoTotal FROM Ordem
WHERE Urgente = 'TRUE' AND PreçoTotal > (SELECT MAX(PreçoTotal) FROM Ordem WHERE Urgente = 'FALSE');

--n. A loja que teve menos valor faturado no último mês.
SELECT TOP 1 Ordem.LojaID, PreçoTotal AS FaturamentoUltimosTrintaDias FROM Ordem
WHERE Dataordem  >= DATEADD(day,-30, GETDATE())
ORDER BY PreçoTotal ASC






