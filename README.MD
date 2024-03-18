# How to run

Clone this repository.

Open a terminal in the directory of the cloned project.

Run the commands

```bash
donet restore
```

```bash
donet ef database update
```

```bash
donet run
```

# Seed and Tests

cd to .k6/

```bash
npm install
```

To create data on database and store json files to be used in tests run:

```bash
npm run seed
```

To run k6 tests cd to.k6/tests an run(change the file test as you need)

```bash
k6 run postPaymentTest.js
```

# Monitoring

Open a terminal in monitoring folder

Change the internal docker ip on prometheus.yml

```bash
docker compose up -d
```

