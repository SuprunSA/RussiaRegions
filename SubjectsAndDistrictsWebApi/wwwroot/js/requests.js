const api = 'https://localhost:5001/api/';

function createRequest(path, method, body) {
    const url = new URL(path, api).href;
    const headers = new Headers();
    headers.append("Content-Type", "application/json;charset=utf-8");
    body = JSON.stringify(body);
    return new Request(url, {
        method,
        headers,
        body
    });
};

export class HttpStatusError {
    constructor(message, status) {
        this.message = message;
        this.status = status;
    }
};

export async function apiRequest(path, method, body) {
    const response = await fetch(createRequest(path, method, body));
    if (!response.ok) {
        const errorMes = response.statusText;
        console.error(errorMes);
        throw new HttpStatusError(errorMes, response.status);
    }
    try {
        return await response.json();
    } catch {
        return null;
    }
};

export const apiMethods = {
    get: (path) => apiRequest(path, 'GET'),
    put: (path, body) => apiRequest(path, 'PUT', body),
    post: (path, body) => apiRequest(path, 'POST', body),
    delete: (path) => apiRequest(path, 'DELETE'),
};