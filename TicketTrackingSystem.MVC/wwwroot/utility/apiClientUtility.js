// apiClient.js
// You can use global variable to change the base url dynamically
// Define the fixed base URL here.
const BASE_URL = 'https://localhost:7264';

export const apiClient = async (endpoint, method = 'GET', data = null) => {
    // Prepend the base URL to the endpoint.
    const url = `${BASE_URL}${endpoint}`;

    const options = {
        method,
        headers: {
            'Content-Type': 'application/json',
        },
    };

    if (data) {
        options.body = JSON.stringify(data);
    }

    const response = await fetch(url, options);

    if (!response.ok) {
        // Optionally, you can handle different status codes or error messages here.
        throw new Error(response.statusText);
    }

    return response.json();
};

export const customApiClient = async (baseUrl,endpoint, method = 'GET', data = null) => {
    // Prepend the base URL to the endpoint.
    const url = `${baseUrl}${endpoint}`;

    const options = {
        method,
        headers: {
            'Content-Type': 'application/json',
        },
    };

    if (data) {
        options.body = JSON.stringify(data);
    }

    const response = await fetch(url, options);

    if (!response.ok) {
        // Optionally, you can handle different status codes or error messages here.
        throw new Error(response.statusText);
    }

    return response.json();
};
