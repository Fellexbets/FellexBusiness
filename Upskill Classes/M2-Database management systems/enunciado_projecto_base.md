# Receitas Eletrónicas
Avaliação: máximo de 15 valores

## Enquadramento
Pretende-se implementar um sistema de informação que permita a gestão de receitas eletrónicas, desde a sua criação em consulta médica até ao seu levantamento na farmácia.

Sobre cada médico é necessário saber o seu nome, número de telefone de contacto, qual a sua especialidade e as várias instituições onde trabalha. O sistema armazena várias informações sobre os pacientes, tais como o nome, morada, número de cartão do cidadão, data de nascimento e número do sistema de saúde. Sobre as farmácias é necessário armazenar o seu nome, morada e telefone.

Os médicos têm a possibilidade de introduzirem as receitas dos pacientes no sistema e, posteriormente, o paciente poderá adquirir na farmácia os medicamentos receitados. O paciente poderá fasear a aquisição dos medicamentos, ou seja, poderá fazer levantamentos parciais em diferentes farmácias.

A receita será inserida por um médico e referir-se-á a um único paciente previamente inserido no sistema. A data de validade da receita deverá ser definida pelo médico.  Para o preenchimento da receita, o médico apenas poderá inserir vários medicamentos (indicando a sua designação, quantidade e posologia). Nas receitas eletrónicas apenas poderão ser inseridos medicamentos conhecidos pelo sistema. Não podem existir receitas que não estejam associadas a um médico e a um paciente.

Quando o paciente se desloca à farmácia para adquirir os medicamentos, o farmacêutico acede à receita através da identificação do cliente, da data da receita ou através da identificação do médico. A receita é exibida com indicação para cada produto ainda não entregue, se o mesmo se encontra ou não disponível para venda na respetiva farmácia. O paciente dá a indicação de quais os medicamentos da receita, disponíveis na farmácia, que pretende comprar, momento a partir do qual o respetivo item da receita é classificado como entregue.

Os medicamentos podem ser de dois tipos: comparticipados ou não. Sobre os medicamentos é necessário saber também o código (único em todo o sistema), a designação, laboratório de fabrico e apresentação (xarope, comprimidos, etc.)

## Objetivos
1.	Crie o Modelo Entidade-Associação e o Modelo Relacional. Tem autonomia para resolver todas as ambiguidades do enunciado.
2.	Preencha as tabelas do Modelo Relacional com, pelo menos, 5 registos cada uma.
3.	Crie as consultas à base de dados (comandos select) que permitam consultar:
    
    a.	O nome, número de cartão do cidadão e número do sistema de saúde de todos pacientes

    b.	As instituições onde o médico Ernesto trabalha.
    
    c.	O stock de todos os medicamentos da Farmácia MedicamentosAqui
    
    d.	As receitas totalmente levantadas, parcialmente adquiridas ou por adquirir do paciente Orlando
    
    e.	As receitas que o médico Faustino introduziu para o paciente Ulisses, bem como a designação, quantidade e posologia de cada medicamento.
    
    f.	Os medicamentos que o médico Ernesto introduziu em receitas, distribuídos por quantidade, nos últimos 5 anos
    
    g.	Os medicamentos que nunca foram utilizados em receitas
    
    h.	O medicamento mais levantado em farmácias
    
    i.	As receitas cuja data de validade já expirou e não foram levantadas
    
    j.	O laboratório de fabrico que disponibiliza mais medicamentos
