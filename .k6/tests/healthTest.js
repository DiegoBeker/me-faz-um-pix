import http from "k6/http";

export const options = {
	scenarios: {
	  spike_usage: {
		executor: "constant-arrival-rate",
		duration: "60s",
		preAllocatedVUs: 100,
		maxVUs: 200,
		rate: 500,
		timeUnit: "1s",
	  },
	},
	thresholds: {
	  "http_reqs{scenario:spike_usage}": ["count>=20000"],
	},
};

export default function () {
	http.get(`http://127.0.0.1:8080/health`);
}