﻿const departmentServiceProxy = new Proxy({}, {
    get: function (target, prop) {
        return function (...args) {
            const serializedArgs = args.map(arg =>
                typeof arg === "object" ? JSON.stringify(arg) : arg
            );

            return fetch('https://localhost:7264/department/call', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    Method: prop,
                    Parameters: serializedArgs,
                }),
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error("Network response was not ok: " + response.statusText);
                    }
                    return response.json();
                })
                .catch(error => {
                    console.error(`Error calling service method "${prop}":`, error);
                    throw error; // Rethrow to handle in calling code
                });
        };
    },
});

export const createDepartmentAsync = (createDepartmentDto) =>
    departmentServiceProxy.createdepartmentasync(createDepartmentDto);
export const deleteDepartmentAsync = (departmentId) =>
    departmentServiceProxy.deletedepartmentasync(departmentId);
export const updateDepartmentAsync = (updateDepartmentDto) =>
    departmentServiceProxy.updatedepartmentasync(updateDepartmentDto);
export const getAllDepartmentsAsHtmlAsync = () =>
    departmentServiceProxy.getalldepartmentsashtmlasync();

export default departmentServiceProxy;
