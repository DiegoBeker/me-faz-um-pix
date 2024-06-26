import http from "k6/http";
import { sleep } from 'k6';
import { SharedArray } from "k6/data";

export const options = {
    scenarios: {
      spike_usage: {
        executor: "constant-arrival-rate",
        duration: "60s",
        preAllocatedVUs: 10,
        maxVUs: 30,
        rate: 42,
        timeUnit: "1s",
      },
    },
    thresholds: {
      "http_reqs{scenario:spike_usage}": ["count>=2500"],
    },
};

const pspsData = new SharedArray("psps", function () {
    const result = JSON.parse(open("../seed/existing_psps.json"));
    return result;
});

const pixKeysdata = new SharedArray("pixKeys", function () {
    const result = JSON.parse(open("../seed/existing_pixKeys.json"));
    return result;
});

export default function () {
    const randomOrigin = pixKeysdata[Math.floor(Math.random() * pixKeysdata.length)];
    const randomDestiny = pixKeysdata[Math.floor(Math.random() * pixKeysdata.length)];
    const randomPsp = pspsData[Math.floor(Math.random() * pspsData.length)];
    const payment = {
        origin: {
            user: {
                cpf: randomOrigin.Cpf
            },
            account: {
                number: randomOrigin.Number,
                agency: randomOrigin.Agency
            }
        },
        destiny: {
            key: {
                value: randomDestiny.Value,
                type: randomDestiny.PixType
            }
        },
        amount: Math.floor(Math.random() * 90000) + 100,
        description: "teste"
    }

    const body = JSON.stringify(payment);
    const headers = {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${randomPsp.Token}`
    };

    const response = http.post(`http://localhost:8080/payment`, body, { headers });

    if (response.status >= 400) {
        console.log(response.body);
    }
    sleep(1)
}
