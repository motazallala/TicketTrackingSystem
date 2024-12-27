const userServiceProxy = new Proxy({}, {
    get: function (target, prop) {
        return function (...args) {
            const serializedArgs = args.map(arg =>
                typeof arg === "object" ? JSON.stringify(arg) : arg
            );

            return fetch('https://localhost:7264/user/call', {
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
export const setRoleToUserAsync = (userId, roleId) =>
    userServiceProxy.setroletouserasync(userId, roleId);



export const createUserAsync = (createUserDto) =>
    userServiceProxy.createuserasync(createUserDto);


export const getUserTypeDropdown = () =>
    userServiceProxy.getusertypedropdown();

export const removeRoleFromUserAsync = (userId, roleId) =>
    userServiceProxy.removerolefromuserasync(userId, roleId);

export const deleteUserAsync = (userId) =>
    userServiceProxy.deleteuserasync(userId);
export const updateUserAsync = (updateUserDto) =>
    userServiceProxy.updateuserasync(updateUserDto);
export const getUserByIdasync = (userId) =>
    userServiceProxy.getuserbyidasync(userId);

export default userServiceProxy;

