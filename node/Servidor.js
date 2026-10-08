const http = require("node:http");
const puerto = 3000;

var archivo = 
{
    papitas: [
        {
            nombre: "Incognito",
            precio: 30,
            marca: "Doritos"
        },
        {
            nombre: "Originales",
            precio: 17,
            marca: "Takis"
        },
    ]
}

const server = http.createServer((request, response) => {
    if (request.method == "GET"){
        response.statusCode = 200;
        response.setHeader("Content-Type", "application/json");
        response.end(JSON.stringify(archivo));
        }
});

server.listen(puerto, () => {
    console.log("Servidor a la escucha en http://localhost:" + puerto);
});