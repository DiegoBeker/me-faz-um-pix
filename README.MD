# How to run

Clone this repository.

This project need at least one consumer PIX, one consumer-concilliation and the PSP mock app running.


- [PSP MOCK](https://github.com/DiegoBeker/psp-mock)
- [CONSUMER-PIX](https://github.com/DiegoBeker/consumer-pix)
- [CONSUMER-CONCILLIATION](https://github.com/DiegoBeker/consumer-concilliation)


Open a terminal in the directory of the cloned project.

Change the internal docker ip on prometheus.yml

Run the commands

```bash
docker compose up -d
```

The application will be initialized with all services. PostreSQL, RabbitMQ, Grafana, Prometheus.

Also will be apllied the migrations.


# Seed and Tests

cd to .k6/

```bash
npm install
```

To create data on database and store json files to be used in tests run:

```bash
npm run seed
```

IMPORTANT: Save the PSP token that will be printed on console. This can be used for authentication and to concilliaton.

To run k6 tests cd to.k6/tests an run(change the file test as you need)

```bash
k6 run postPaymentTest.js
```



