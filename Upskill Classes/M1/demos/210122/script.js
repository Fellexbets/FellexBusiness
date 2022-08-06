function httpGet(theUrl)
{
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open( "GET", theUrl, false ); 
    xmlHttp.send( null );
    return xmlHttp.responseText;
}

function start(){
    // Pedido síncrono é desaconselhado. Iremos utilizar apenas nesta fase.
    const response = httpGet("https://www.googleapis.com/books/v1/volumes?q=harry+potter");
    // Consultar: https://www.json.org/json-en.html | https://developer.mozilla.org/pt-BR/docs/Web/JavaScript/Reference/Global_Objects/JSON/parse
    const data = JSON.parse(response);
    for(const item of data.items){
        if(item.volumeInfo.imageLinks){
            let conteudo = document.getElementById("conteudo");
            conteudo.innerHTML += `<h1>${item.volumeInfo.title}</h1>`;
            conteudo.innerHTML += `<h4>${item.volumeInfo.authors}<h4>`;
            conteudo.innerHTML += `<p>${item.volumeInfo.description}</p>`;
            conteudo.innerHTML += `<img src=${item.volumeInfo.imageLinks.thumbnail}>`;
        }
    }
}

