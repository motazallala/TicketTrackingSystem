const ticketServiceProxy = new Proxy({}, {
    get: function (target, prop) {
        return function (...args) {
            const serializedArgs = args.map(arg =>
                typeof arg === "object" ? JSON.stringify(arg) : arg
            );

            return fetch('https://localhost:7264/ticket/call', {
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

export const getTicketByIdAsync = (ticketId) =>
    ticketServiceProxy.getticketbyidasync(ticketId);

export const removeTicketFromUserAsync = (ticketId) =>
    ticketServiceProxy.removeticketfromuserasync(ticketId);

//export const assignTicketToUserAsync = (ticketId) =>
//    ticketServiceProxy.assigntickettouserasync(ticketId);
export const assignTicketToUserAsync = (ticketId, estimationTime) =>
    ticketServiceProxy.assigntickettouserasync(ticketId, estimationTime);


export const setEstimatedCompletionDateForReassignTicketAsync = (ticketId, estimationTime) =>
    ticketServiceProxy.setestimatedcompletiondateforreassignticketasync(ticketId, estimationTime);
export const updateTicketWithAutoStageAsync = (ticketId, status, isFinished, message) =>
    ticketServiceProxy.updateticketwithautostageasync(ticketId, status, isFinished, message);

export const addTicketAsync = (ticketDto) =>
    ticketServiceProxy.addticketasync(ticketDto);

export const getTicketStatusDropdown = () =>
    ticketServiceProxy.getticketstatusdropdown();

export const getAllFreeMembersDropdownAsync = (projectId) =>
    ticketServiceProxy.getallfreemembersdropdownasync(projectId);

export const checkEstimatedCompletionDateAsync = (ticketId) =>
    ticketServiceProxy.checkestimatedcompletiondateasync(ticketId);

export const reAssignTicketAsync = (ticketId, userId) =>
    ticketServiceProxy.reassignticketasync(ticketId, userId);