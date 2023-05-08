const fastify = require("fastify")({ logger: true });
let _name = "default";
fastify.get("/", (request, reply) => {
  reply.send({ name:_name });
});
fastify.put("/name", (request, reply) => {
  const { name } = request.body;
  _name = name;
  reply.send({ name:_name });
});
fastify.listen(3000, (err) => {
  if (err) {
    fastify.log.error(err);
    process.exit(1);
  }
});
