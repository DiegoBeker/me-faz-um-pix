const dotenv = require("dotenv");
const fs = require("fs");
const { faker } = require('@faker-js/faker');

dotenv.config();

const knex = require("knex")({
  client: "pg",
  connection: process.env.DATABASE_URL,
});

console.log(process.env.DATABASE_URL)
const AMOUNT_TO_CREATE = 1_000_000;
const ERASE_DATA = false;
let data = [];

async function run() {
  if (ERASE_DATA) {
    await knex("User").del();
    await knex("PaymentProvider").del();
    await knex("PaymentProviderAccount").del();
  }

  const start = new Date();

  // users
  data = generateUsers();
  await populateDb("User" , data);
  generateJson("./seed/existing_users.json", data);

  //Psps
  data = generatePsps();
  await populateDb("PaymentProvider" , data);
  generateJson("./seed/existing_psps.json", data);
  
  //Accounts
  data= generateAccounts();
  await populateDb("PaymentProviderAccount" , data);
  generateJson("./seed/existing_accounts.json", data);

  data = generatePixKeys();
  await populateDb("PixKey" , data);
  console.log("geting data");
  data = await getPixKeysWithAccountsAndUsers()
  generateJson("./seed/existing_pixKeys.json", data);

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
      Token: faker.string.uuid(),
      Name: faker.company.name()
    });
  }

  return psps;
}


function generateAccounts() {
  console.log(`Generating ${AMOUNT_TO_CREATE} Accoounts...`);
  const accounts = [];
  for (let i = 0; i < AMOUNT_TO_CREATE; i++) {
    accounts.push({
      Agency: faker.string.numeric({length: 5, min: 10000, max:99999}),
      Number: faker.string.numeric({length: 9}),
      UserId: Math.floor(Math.random() * AMOUNT_TO_CREATE + 1),
      PaymentProviderId: Math.floor(Math.random() * AMOUNT_TO_CREATE + 1),
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

function generatePixKeys() {
  console.log(`Generating ${AMOUNT_TO_CREATE} Keys...`);
  const pixKeys = [];
  for (let i = 0; i < AMOUNT_TO_CREATE; i++) {
    pixKeys.push({
      Value: faker.string.uuid(),
      PixType: "Random",
      PaymentProviderAccountId: Math.floor(Math.random() * AMOUNT_TO_CREATE + 1),
    });
  }

  return pixKeys;
}

async function getPixKeysWithAccountsAndUsers() {
  let pixKeysWithAccountsAndUsers = [];
  try {
    const query = `
      SELECT "PixKey".*, "PaymentProviderAccount".*, "User".*
      FROM "PixKey"
      INNER JOIN "PaymentProviderAccount" ON "PixKey"."PaymentProviderAccountId" = "PaymentProviderAccount"."Id"
      INNER JOIN "User" ON "PaymentProviderAccount"."UserId" = "User"."Id"
    `;

    pixKeysWithAccountsAndUsers = await knex.raw(query);

    console.log('PixKeys com suas PaymentProviderAccounts e Users:', pixKeysWithAccountsAndUsers.rows.length);
  } catch (error) {
    console.error('Erro ao buscar PixKeys com suas PaymentProviderAccounts e Users:', error);
  }
  return pixKeysWithAccountsAndUsers.rows;
}