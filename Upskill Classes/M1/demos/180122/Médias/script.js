alert("Escreva o número de notas");
numero_notas = prompt();

if(numero_notas <= 0){
    alert("Os valores deveriam ser positivos");
}
else{
    contador = 0;
    acumulado = 0;
    while(contador < numero_notas){
        alert("Introduza nota");
        nota_em_texto = prompt(); 
        nota_em_numero = Number(nota_em_texto);
        acumulado = acumulado + nota_em_numero;
        contador = contador + 1;
    }
    media = acumulado / numero_notas;
    alert(media);
}