//you can use this dy adding this modal for the bady of the page
//< !--Modal -->
//    <div id="myModal" class="modal fade" role="dialog">
//        <div class="modal-dialog">

//            <!-- Modal content-->
//            <div class="modal-content">
//                <div class="modal-header" id="modalHeader">
//                    <h4 class="modal-title" id="modalTitle">Row information</h4>
//                </div>
//                <div class="modal-body" id="modelBody">
//                </div>
//                <div class="modal-footer" id="modal-footer-data">
//                </div>
//            </div>

//        </div>
//    </div>
export function setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, footerButtons) {
    // Clear existing modal content
    modalTitle.text('');
    modalBody.empty();
    modalFooter.empty();

    // Set the new modal content
    modalTitle.text(title);
    modalBody.append(bodyContent);

    // Add buttons to the modal footer
    footerButtons.forEach(button => modalFooter.append(button));
}

export function showModal(title, description) {
    const modalTitle = $('.modal .modal-title');
    const modalBody = $('.modal .modal-body');
    const modalFooter = $('.modal .modal-footer');
    const closeButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
    setupModalData(modalTitle, modalBody, modalFooter, title, description, [closeButton]);
    $('#myModal').modal('show');
}
export function showSuccessModal(title, description) {
    const modalTitle = $('.modal .modal-title');
    const modalBody = $('.modal .modal-body');
    const modalFooter = $('.modal .modal-footer');
    const bodyContent = `<p>${description}</p>`;
    const closeButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
    setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [closeButton]);
    $('#myModal').modal('show');    
}
// make function to show modal with error message
export function showErrorModal(errorMessage) {
    const modalTitle = $('.modal .modal-title');
    const modalBody = $('.modal .modal-body');
    const modalFooter = $('.modal .modal-footer');
    const title = 'Error';
    const bodyContent = `Error Description <p>${errorMessage}</p>`;
    const closeButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
    setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [closeButton]);
    $('#myModal').modal('show');
}
export function showErrorModalWithTitle(title,errorMessage) {
    const modalTitle = $('.modal .modal-title');
    const modalBody = $('.modal .modal-body');
    const modalFooter = $('.modal .modal-footer');
    const titlein = 'Error : ' + title;
    const bodyContent = `Error Description <p>${errorMessage}</p>`;
    const closeButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
    setupModalData(modalTitle, modalBody, modalFooter, titlein, bodyContent, [closeButton]);
    $('#myModal').modal('show');
}
export default {
    setupModalData,
    showErrorModal,
    showModal,
    showSuccessModal,
    showErrorModalWithTitle

};