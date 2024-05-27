import { RequestConfig } from "@umijs/max";
import Password from "antd/es/input/Password";

export async function getInitialState() {
    console.log("init app");

    return {
        username: ''
    }
}

export const request: RequestConfig = {
    timeout: 15000,
    errorConfig: {
        errorHandler: () => {},
        errorThrower: () => {},
    },
    requestInterceptors: [
        (config : any) => {
            const username = localStorage.getItem('username');
            const password = localStorage.getItem('password');

            const authHeaders = { 'Authorization': username + ' ' + password }
            config.headers = { ...config.headers, ...authHeaders };

            return config
        },
    ],
    responseInterceptors: [],
}