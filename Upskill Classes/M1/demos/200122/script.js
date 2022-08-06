function media(numero_notas, notas){    // argumentos: entradas
    let acumulado = 0;
    // v1: for(let index = 0; index < numero_notas; ++index){ // index = index + 1
    for(const nota of notas){
        // v1: acumulado += notas[index]; // acumulado = acumulado + notas[index]
        acumulado += nota;
    }
    let media = acumulado / numero_notas;
    return media; // saídas
}

function cliquei(){
    let mediaDoAluno = media(8, [10, 10, 10, 14, 15, 16, 17, 10]);
    let numero_aluno = document.getElementById("num_aluno").value;
    let color = mediaDoAluno > 9.5 ? "verde" : "vermelho";
    // `` Interpolação de Strings
    document.getElementById("res").innerHTML = `<h1 class='${ color }'>A média do aluno ${ numero_aluno } é: ${ mediaDoAluno } valores</h1>`;
}
