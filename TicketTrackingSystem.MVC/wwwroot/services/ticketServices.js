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
export const addTicketAsync = (ticketDto) =>
    ticketServiceProxy.addticketasync(ticketDto);
export const updateTicketStatusAsync = (ticketDto) =>
    ticketServiceProxy.updateticketstatusasync(ticketDto);
export const updateTicketStageAsync = (ticketId, stage) =>
    ticketServiceProxy.updateticketstageasync(ticketId, stage);
export const updateTicketMessageAsync = (ticketId, message) =>
    ticketServiceProxy.updateticketmessageasync(ticketId, message);
export const updateTicketStatusWithAutoStageAsync = (ticketId, status, isFinished) =>
    ticketServiceProxy.updateticketstatuswithautostageasync(ticketId, status, isFinished);
export const getTicketStatusDropdown = () =>
    ticketServiceProxy.getticketstatusdropdown();

export const updateTicketWithAutoStageAsync = (ticketId, status, isFinished, message) =>
    ticketServiceProxy.updateticketwithautostageasync(ticketId, status, isFinished, message);
