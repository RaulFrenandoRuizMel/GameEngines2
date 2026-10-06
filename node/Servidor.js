const http = require("node:http");
const puerto = 3000;

const server = http.createServer((request, response) => {
    response.statusCode = 200;
    response.setHeader("Content-Type", "application/json");
    const objeto_respuesta = {
        id: 384
    }
    response.end(JSON.stringify(objeto_respuesta));
});

server.listen(puerto, () => {
    console.log("Servidor a la escucha en http://localhost:" + puerto);
});