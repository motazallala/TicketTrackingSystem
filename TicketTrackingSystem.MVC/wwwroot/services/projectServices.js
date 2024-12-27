const projectServiceProxy = new Proxy({}, {
    get: function (target, prop) {
        return function (...args) {
            const serializedArgs = args.map(arg =>
                typeof arg === "object" ? JSON.stringify(arg) : arg
            );

            return fetch('https://localhost:7264/project/call', {
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

export const createProjectAsync = (createProjectDto) =>
    projectServiceProxy.createprojectasync(createProjectDto);

export const updateProjectAsync = (updateProjectDto) =>
    projectServiceProxy.updateprojectasync(updateProjectDto);

export const deleteProjectAsync = (projectId) =>
    projectServiceProxy.deleteprojectasync(projectId);