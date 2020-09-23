// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$('table td a, .modal-href').on('click', function (event) {
    event.preventDefault();
    var recipient = $(this).attr('href') // Extract info from data-* attributes
    fetch(recipient).then(function (response) {
        return response.text();
    }).then(function (html) {
        // Convert the HTML string into a document object
        var parser = new DOMParser();
        var doc = parser.parseFromString(html, 'text/html');
        // Get the form
        let form = doc.querySelector('#modal-content');
        if (!form) {
            window.location.href = recipient;
        }

        //console.log(form);
        let modal = document.getElementById("genericModal");
        if (!modal) {
            let genericModal = document.createElement("div");
            genericModal.setAttribute("id", "genericModal");
            genericModal.className = "modal fade";
            genericModal.setAttribute("tabindex", "-1");
            genericModal.setAttribute("role", "dialog");
            genericModal.setAttribute("aria-hidden", "true");
            let genericModalInner = document.createElement("div");
            genericModalInner.className = "modal-dialog modal-dialog-centered";
            genericModalInner.setAttribute("role", "document");
            let genericModalContent = document.createElement("div");
            genericModalContent.className = "modal-content";
            let genericModalHeader = document.createElement("div");
            genericModalHeader.className = "modal-header";
            genericModalHeader.innerHTML = `  <h3 class="modal-title" id="exampleModalLabel"></h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>`;

            let genericModalBody = document.createElement("div");
            genericModalBody.className = "modal-body";


            genericModalContent.appendChild(genericModalHeader);
            genericModalBody.appendChild(form);
            genericModalContent.appendChild(genericModalBody);

            genericModalInner.appendChild(genericModalContent);
            genericModal.appendChild(genericModalInner);
            document.getElementsByTagName("body")[0].append(genericModal);
        } else {
            document.querySelector("#genericModal .modal-body").innerHTML = "";
            document.querySelector("#genericModal .modal-body").appendChild(form);
        }

        $("#genericModal").modal("show");

    }).catch(function (err) {
        // There was an error
        console.warn('Something went wrong.', err);
    });


});

