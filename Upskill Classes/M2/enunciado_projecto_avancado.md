# Cadeia de retrosarias
Avaliação: máximo de 20 valores

## Enquadramento
Uma cadeia de retrosarias pretende introduzir um sistema de informação que suporte a sua atividade, nomeadamente o registo dos trabalhos solicitados pelos clientes e respetivo acompanhamento até à emissão do recibo.

O Gestor da cadeia pretende ter a possibilidade de monitorizar a atividade das lojas e efetuar algumas operações de gestão (por exemplo, efetuar encomendas a fornecedores).

Os pedidos solicitados pelos clientes resultam de combinações de dois elementos: peça e arranjo. Há vários tipos de peças (camisa, casaco, calças, etc.) e vários tipos de arranjos (coser, bainhas, colocar fechos, etc.). Cada solicitação de execução de um trabalho é descrita através da especificação do tipo de arranjo e da peça de vestuários, sendo necessário fazer uma caracterização da eça (cor, tamanho, etc.) e ainda, em alguns casos, uma descrição breve sobre o arranjo a executar. Cada pedido de execução pode conter um ou vários arranjos.

Os vários arranjos de peças têm um custo tabelado (sendo que poderão existir taxas para trabalhos urgentes), podendo surgir situações não habituais em que seja necessário elaborar um orçamento. O trabalho apenas é iniciado depois de o cliente aceitar o orçamento. Quando o pedido está finalizado, deve ser marcado como tal.

Os trabalhos tabelados são pagos no momento em que o cliente entrega as peças. Os restantes são pagos no momento em que o cliente levanta as peças.

A aplicação deve englobar a emissão de recibos, e o registo de devoluções com o respetivo reembolso.

## Objetivos
1. Crie o Modelo Entidade-Associação e o Modelo Relacional. Tem autonomia para resolver todas as ambiguidades do enunciado.
2.	Preencha as tabelas do Modelo Relacional com pelo menos 5 registos cada uma.
3.	Crie as consultas à base de dados (comandos select) que permitam consultar:

    a.	Todas as lojas da zona de Lisboa

    b.	Informação sobre os trabalhos não concluídos, por ordem crescente de data de início

    c.	Os trabalhos urgentes aceites pelo cliente

    d.	O arranjo mais caro

    e.	O custo do arranjo das peças, por tipo de arranjo

    f.	O tipo de arranjo que nunca foi pedido

    g.	A loja que tem mais pedidos entregues

    h.	A quantidade de peças arranjadas da loja 1, dos últimos 7 dias

    i.	Os trabalhos que incluem arranjos de camisas e que ainda não foram levantados

    j.	Os trabalhos que não foram pagos e foram devolvidos

    k.	Os pedidos urgentes que incluem peças com pelo menos 2 cores diferentes

    l.	Os trabalhos não devolvidos com a indicação nas observações de que são peças sensíveis

    m.	Os trabalhos urgentes que foram mais caros do que todos os trabalhos não urgentes.

    n.	A loja que teve menos valor faturado no último mês.
