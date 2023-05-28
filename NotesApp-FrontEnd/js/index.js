function mainFunction() {

    const BASE_URL = "https://localhost:7076/api/Note";

    checkIfUserIsLogged();

    function checkIfUserIsLogged() {

        if (sessionStorage.hasOwnProperty("username")) {

            let registerAnchor = document.querySelector(".authentication-links .authentication-link:nth-child(2)");

            let loginAnchor = document.querySelector(".authentication-links .authentication-link:nth-child(1)");

            let ul = document.querySelector(".authentication-links");

            //delete the login and register anchors
            ul.removeChild(loginAnchor);
            ul.removeChild(registerAnchor);

            //add hello message
            let greetingMessageSpan = document.createElement("span");

            greetingMessageSpan.textContent = `Hello, ${sessionStorage.getItem("username")}!`;

            ul.appendChild(greetingMessageSpan);

            //add logout button
            let logoutButton = document.createElement("button");

            logoutButton.textContent = "Logout";

            logoutButton.classList.add("logoutBtn");

            logoutButton.addEventListener("click", logOut);

            ul.appendChild(logoutButton);

            //hide not logged in section 
            let notLoggedInSection = document.querySelector(".not-logged-in-section");

            notLoggedInSection.classList.add("hidden");

            let loggedInSection = document.querySelector(".logged-in-section");

            loggedInSection.classList.remove("hidden");

            //add username to heading
            let heading = document.createElement("h1");

            heading.textContent = `Welcome to NotesApp, ${sessionStorage.getItem("username")}!`;

            loggedInSection.appendChild(heading);

            loadAllUserTasks();

        } else {

        }

    }

    function addNoteSidebar(event) {

        //check if there is another sidebar opened already
        if (document.querySelector("body aside") !== null) {

            removeSideBar();
        }

        //add aside between header and main
        let aside = createAsideElement();

        let body = document.querySelector("body");

        let main = document.querySelector("main");

        body.insertBefore(aside, main);

        let addButton = aside.querySelector("button");
        addButton.textContent = "Add";
        addButton.classList.add("addBtn");
        addButton.addEventListener("click", addNote);

    }

    async function addNote(event) {

        event.preventDefault();

        try {

            let title = event.target.parentElement.querySelector(".title-container > input").value;
            let description = event.target.parentElement.querySelector(".description-container > textarea").value;

            let note = {
                title: title,
                description: description
            };

            let postMethod = {
                method: "POST",
                headers: {
                    "content-type": "application/json"
                },
                body: JSON.stringify(note)
            };

            let addNoteResponse = await fetch(`${BASE_URL}/add/${sessionStorage.getItem("userId")}`, postMethod);

            if (addNoteResponse["status"] === 400) {

                let addNoteResponseJson = await addNoteResponse.json();

                printErrors(addNoteResponseJson);

            } else {

                loadAllUserTasks();

                removeSideBar();

            }

        } catch (error) {

            console.error(error);

        }

    }

    function printErrors(responseJson) {

        let errorsContainer = document.querySelector(".sidebar .errors-container");

        errorsContainer.innerHTML = null;

        if (responseJson["errors"].hasOwnProperty("Title")) {

            //title errors
            for (const titleError of responseJson["errors"]["Title"]) {

                let span = document.createElement("span");

                span.textContent = titleError;

                span.classList.add("error");

                errorsContainer.appendChild(span);

            }
        }
        if (responseJson["errors"].hasOwnProperty("Description")) {

            //description errors
            for (const descriptionError of responseJson["errors"]["Description"]) {

                let span = document.createElement("span");

                span.textContent = descriptionError;

                span.classList.add("error");

                errorsContainer.appendChild(span);

            }
        }
    }

    async function deleteNote(event) {
        event.preventDefault();

        try {

            let deleteMethod = {
                method: "DELETE"
            };

            let deleteMethodResponse = await fetch(`${BASE_URL}/delete/${sessionStorage.getItem("userId")}/${sessionStorage.getItem("currentNoteId")}`, deleteMethod);

        } catch (error) {

            console.error(error);

        }

        removeSideBar();

        loadAllUserTasks();
    }

    async function editNote(event) {
        event.preventDefault();

        try {

            let titleInput = event.target.parentElement.parentElement.querySelector(".title-container > #title");
            let descriptionInput = event.target.parentElement.parentElement.querySelector(".description-container > #description");

            let note = {
                title: titleInput.value,
                description: descriptionInput.value
            };

            let patchMethod = {
                method: "PATCH",
                headers: {
                    "content-type": "application/json"
                },
                body: JSON.stringify(note)
            };

            let patchMethodResponse = await fetch(`${BASE_URL}/edit/${sessionStorage.getItem("userId")}/${sessionStorage.getItem("currentNoteId")}`, patchMethod);

            if (patchMethodResponse["status"] === 400) {

                let patchMethodResponseJson = await patchMethodResponse.json();

                printErrors(patchMethodResponseJson);

            } else {

                removeSideBar();

                loadAllUserTasks();

            }

        } catch (error) {

            console.error(error);

        }
    }

    function removeSideBar() {

        let sidebar = document.querySelector("aside");

        sidebar.remove();

    }

    function createAsideElement() {

        let aside = document.createElement("aside");

        let form = document.createElement("form");
        form.classList.add("sidebar-form");

        //title section

        let titleContainer = document.createElement("div");
        titleContainer.classList.add("title-container");

        let titleLabel = document.createElement("label");
        titleLabel.textContent = "Title:";
        titleLabel.for = "title";
        titleLabel.style.display = "block";
        titleLabel.style.marginBottom = "10px"
        titleContainer.appendChild(titleLabel);

        let titleInput = document.createElement("input");
        titleInput.id = "title";
        titleContainer.appendChild(titleInput);

        form.appendChild(titleContainer);

        //description section

        let descriptionContainer = document.createElement("div");
        descriptionContainer.classList.add("description-container");

        let descriptionLabel = document.createElement("label");
        descriptionLabel.textContent = "Description:";
        descriptionLabel.for = "description";
        descriptionLabel.style.display = "block";
        descriptionLabel.style.marginBottom = "10px"
        descriptionContainer.appendChild(descriptionLabel);

        let descriptionTextArea = document.createElement("textarea");
        descriptionTextArea.id = "description";
        descriptionTextArea.style.minHeight = "80px";
        descriptionContainer.appendChild(descriptionTextArea);

        form.appendChild(descriptionContainer);

        //error container section

        let errorsContainer = document.createElement("div");
        errorsContainer.classList.add("errors-container");
        form.appendChild(errorsContainer);

        //submit button

        let button = document.createElement("button");
        button.style.display = "block";
        form.appendChild(button);

        aside.appendChild(form);
        aside.classList.add("sidebar");

        aside.style.height = `${document.querySelector("body").offsetHeight}px`;

        return aside;

    }

    async function loadAllUserTasks() {

        if (document.querySelector(".notes-section") !== null) {

            let notesSection = document.querySelector(".notes-section");

            notesSection.remove();

        }

        let notesSection = document.createElement("section");
        notesSection.classList.add("notes-section");

        let addContainer = document.createElement("div");
        addContainer.classList.add("note-container");
        addContainer.addEventListener("click", addNoteSidebar);

        let plusSign = document.createElement("p");
        plusSign.textContent = "+";

        let addNoteSpan = document.createElement("span");
        addNoteSpan.textContent = "Add a task";

        addContainer.appendChild(plusSign);
        addContainer.appendChild(addNoteSpan);

        let loggedInSection = document.querySelector(".logged-in-section");

        notesSection.appendChild(addContainer);

        try {

            let response = await fetch(`https://localhost:7076/api/Note/${sessionStorage.getItem("userId")}`, { method: "GET" });

            let responseJson = await response.json();

            for (const currentNote of responseJson) {

                let title = currentNote["title"];
                let description = currentNote["description"];

                let noteContainer = document.createElement("div");
                noteContainer.classList.add("note-container");
                noteContainer.setAttribute("data-id", currentNote.id);

                let titleHeading = document.createElement("h2");
                titleHeading.textContent = title;
                noteContainer.appendChild(titleHeading);

                let descriptionParagraph = document.createElement("p");
                descriptionParagraph.textContent = description;
                noteContainer.appendChild(descriptionParagraph);

                noteContainer.addEventListener("click", noteSidebar)

                notesSection.appendChild(noteContainer);

            }

            loggedInSection.appendChild(notesSection);

        } catch (error) {

            console.error(error);

        }
    }

    async function noteSidebar(event) {

        if (document.querySelector("body aside") !== null) {

            removeSideBar();
        }

        //add aside between header and main
        let aside = createAsideElement();

        let body = document.querySelector("body");

        let main = document.querySelector("main");

        body.insertBefore(aside, main);

        let btnsContainer = document.createElement("div");
        btnsContainer.classList.add("btnsContainer");

        let editButton = aside.querySelector("button");
        editButton.textContent = "Edit";
        editButton.classList.add("editBtn");
        editButton.addEventListener("click", editNote);
        btnsContainer.appendChild(editButton);

        let form = aside.querySelector("form");

        let deleteButton = document.createElement("button");
        deleteButton.textContent = "Delete";
        deleteButton.classList.add("deleteBtn");
        deleteButton.addEventListener("click", deleteNote);
        btnsContainer.appendChild(deleteButton);

        form.appendChild(btnsContainer);

        let titleInput = form.querySelector(".title-container > #title");
        let descriptionInput = form.querySelector(".description-container > #description");

        titleInput.value = event.target.querySelector("h2").textContent;
        descriptionInput.value = event.target.querySelector("p").textContent;

        sessionStorage.setItem("currentNoteId", event.target.getAttribute("data-id"));

    }

    function logOut(event) {

        sessionStorage.removeItem("username");

        let notLoggedInSection = document.querySelector(".not-logged-in-section");

        notLoggedInSection.classList.remove("hidden");

        let loggedInSection = document.querySelector(".logged-in-section");

        loggedInSection.classList.add("hidden");

        location.reload();

    }

}

mainFunction();