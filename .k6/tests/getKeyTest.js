import http from "k6/http";
import { SharedArray } from "k6/data";

export const options = {
    scenarios: {
      spike_usage: {
        executor: "constant-arrival-rate",
        duration: "60s",
        preAllocatedVUs: 10,
        maxVUs: 50,
        rate: 350,
        timeUnit: "1s",
      },
    },
    thresholds: {
      "http_reqs{scenario:spike_usage}": ["count>=20000"],
    },
};

const keysData = new SharedArray("users", function () {
    const result = JSON.parse(open("../seed/existing_pixKeys.json"));
    return result;
});

export default function () {
    const randomkey = keysData[Math.floor(Math.random() * keysData.length)];
    const randomPsp = "66m05vwhEAeCU6hCHg641H7l5CbJu8F2XxYFPU6JTVpCJMOWbhEEaGLYaySxsM39";
    const key = {
        value: randomkey.Value,
        type: randomkey.PixType
    }
    const headers = {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${randomPsp}`
    };

    const response = http.get(`http://127.0.0.1:8080/keys/${randomkey.PixType}/${randomkey.Value}`, { headers });

    if (response.status >= 400) {
        console.log(response.body);
    }
}