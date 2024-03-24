import http from "k6/http";
import { sleep } from 'k6';
import { SharedArray } from "k6/data";
import { uuidv4 } from 'https://jslib.k6.io/k6-utils/1.4.0/index.js';

export const options = {
    scenarios: {
      spike_usage: {
        executor: "constant-arrival-rate",
        duration: "60s",
        preAllocatedVUs: 10,
        maxVUs: 20,
        rate: 100,
        timeUnit: "1s",
      },
    },
    thresholds: {
      "http_reqs{scenario:spike_usage}": ["count>=10000"],
    },
};

const usersData = new SharedArray("users", function () {
    const result = JSON.parse(open("../seed/existing_users.json"));
    return result;
});

const pspsData = new SharedArray("psps", function () {
    const result = JSON.parse(open("../seed/existing_psps.json"));
    return result;
});

export default function () {
    const randomUser = usersData[Math.floor(Math.random() * usersData.length)];
    const randomPsp = pspsData[Math.floor(Math.random() * pspsData.length)];
    const randomUUID = uuidv4();
    const key = {
        key: {
            value: randomUUID,
            type: 'Random'
        },
        user: {
            cpf: randomUser.Cpf,
        },
        account: {
            number: (Math.floor(Math.random() * 90000) + 10000).toString(),
            agency: (Math.floor(Math.random() * 9000) + 1000).toString()
        }
    }
    const body = JSON.stringify(key);
    const headers = {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${randomPsp.Token}`
    };

    const response = http.post(`http://127.0.0.1:8080/keys`, body, { headers });

    if (response.status >= 400) {
        console.log(response.body);
    }

}
