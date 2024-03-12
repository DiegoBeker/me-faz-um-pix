const dotenv = require("dotenv");
const fs = require("fs");
const { faker } = require('@faker-js/faker');

dotenv.config();

const knex = require("knex")({
  client: "pg",
  connection: process.env.DATABASE_URL,
});

console.log(process.env.DATABASE_URL)

const AMOUNT_TO_CREATE = 500_000;
const ERASE_DATA = false;

async function run() {
  if (ERASE_DATA) {
    await knex("User").del();
    await knex("PaymentProvider").del();
    await knex("PaymentProviderAccount").del();
  }

  const start = new Date();

  // users
  const users = generateUsers();
  await populateDb("User" , users);
  //generateJson("./seed/existing_users.json", users);

  //Psps
  const psps = generatePsps()
  await populateDb("PaymentProvider" , psps);
  //generateJson("./seed/existing_psps.json", psps);
  
  //Accounts
  const accounts = generateAccounts(users.length,psps.length);
  await populateDb("PaymentProviderAccount" , accounts);
  //generateJson("./seed/existing_accounts.json", accounts);


  console.log("Closing DB connection...");
  await knex.destroy();

  const end = new Date();

  console.log("Done!");
  console.log(`Finished in ${(end - start) / 1000} seconds`);
}

run();

function generateUsers() {
  console.log(`Generating ${AMOUNT_TO_CREATE} users...`);
  const users = [];
  for (let i = 0; i < AMOUNT_TO_CREATE; i++) {
    users.push({
      CreatedAt: new Date().toISOString(),
      UpdatedAt: new Date().toISOString(),
      Cpf: `${Date.now() + i}`,
      Name: `fakerUser${i}`
    });
  }

  return users;
}

function generatePsps() {
  console.log(`Generating ${AMOUNT_TO_CREATE} Psps...`);
  const psps = [];
  for (let i = 0; i < AMOUNT_TO_CREATE; i++) {
    psps.push({
      CreatedAt: new Date().toISOString(),
      UpdatedAt: new Date().toISOString(),
      Token: faker.string.uuid(),
      Name: faker.company.name()
    });
  }

  return psps;
}


function generateAccounts(users, psps) {
  console.log(`Generating ${AMOUNT_TO_CREATE} Accoounts...`);
  const accounts = [];
  for (let i = 0; i < AMOUNT_TO_CREATE; i++) {
    accounts.push({
      CreatedAt: new Date().toISOString(),
      UpdatedAt: new Date().toISOString(),
      Agency: faker.string.numeric({length: 5, min: 10000, max:99999}),
      Number: faker.string.numeric({length: 9}),
      UserId: Math.floor(Math.random() * users + 1),
      PaymentProviderId: Math.floor(Math.random() * psps + 1),
    });
  }

  return accounts;
}

async function populateDb(tableName, data) {
  console.log(`Storing ${tableName} on DB...`);

  await knex.batchInsert(tableName, data);
}

function generateJson(filepath, data) {
  if (fs.existsSync(filepath)) {
    fs.unlinkSync(filepath);
  }
  fs.writeFileSync(filepath, JSON.stringify(data));
}
