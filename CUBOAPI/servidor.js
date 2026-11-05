const { info } = require("node:console");
const http = require("node:http");
const puerto = 3000;

var numero;

const server = http.createServer((request, response) => {

    response.setHeader("Acces-Control-Allow-Methods", "GET, POST, PUT, DELETE");

    if(request.method == "POST")
    {
        request.on("data", info => {
            numero = info.toString();
            response.statusCode = 200;
            response.setHeader("Content-Type", "text/plain");
            response.end("true");
        });
    }

    console.log("me pidieron algo");
});

server.listen(puerto, () => {
    console.log("Servidor a la escucha en http://localhost:" + puerto);
});