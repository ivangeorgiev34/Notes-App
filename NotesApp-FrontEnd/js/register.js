function mainFunction() {

    let registerBtn = document.querySelector(".registerBtn");
    registerBtn.addEventListener("click", register);
    registerBtn.addEventListener("mouseover", addBoxShadow);
    registerBtn.addEventListener("mouseout", removeBoxShadow);

    let emailInput = document.querySelector("#email");
    let usernameInput = document.querySelector("#username");
    let passwordInput = document.querySelector("#password");
    let passwordRepeatInput = document.querySelector("#password-repeat");

    emailInput.addEventListener("focus", addBoxShadow);
    usernameInput.addEventListener("focus", addBoxShadow);
    passwordInput.addEventListener("focus", addBoxShadow);
    passwordRepeatInput.addEventListener("focus", addBoxShadow);

    emailInput.addEventListener("blur", removeBoxShadow);
    usernameInput.addEventListener("blur", removeBoxShadow);
    passwordInput.addEventListener("blur", removeBoxShadow);
    passwordRepeatInput.addEventListener("blur", removeBoxShadow);

    let errorsContainer = document.querySelector(".errors-containter");

    function addBoxShadow(event) {

        event.target.style.boxShadow = "0px 0px 5px 0px #000000";

    }

    function removeBoxShadow(event) {

        event.target.style.boxShadow = null;

    }


    async function register(event) {

        event.preventDefault();

        if (passwordInput.value === passwordRepeatInput.value) {

            try {

                let registrationObject = {
                    email: emailInput.value,
                    username: usernameInput.value,
                    password: passwordInput.value
                };

                let postMethod = {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(registrationObject)
                };

                let registerResponse = await fetch("https://localhost:7076/api/User/register", postMethod);

                let jsonResponse = await registerResponse.json();

                if (jsonResponse.hasOwnProperty("status")) {

                    errorsContainer.innerHTML = null;

                    if (jsonResponse["errors"].hasOwnProperty("Email")) {

                        for (const currentError of jsonResponse["errors"]["Email"]) {

                            let span = document.createElement("span");

                            span.textContent = currentError;

                            span.style.color = "red";
                            span.style.display = "block";
                            span.style.textAlign = "center";

                            errorsContainer.appendChild(span);

                        }

                    } if (jsonResponse["errors"].hasOwnProperty("Username")) {

                        for (const currentError of jsonResponse["errors"]["Username"]) {

                            let span = document.createElement("span");

                            span.textContent = currentError;

                            span.style.color = "red";
                            span.style.display = "block";
                            span.style.textAlign = "center";

                            errorsContainer.appendChild(span);

                        }

                    } if (jsonResponse["errors"].hasOwnProperty("Password")) {

                        for (const currentError of jsonResponse["errors"]["Password"]) {

                            let span = document.createElement("span");

                            span.textContent = currentError;

                            span.style.color = "red";
                            span.style.display = "block";
                            span.style.textAlign = "center";

                            errorsContainer.appendChild(span);

                        }

                    }

                } else if (jsonResponse.hasOwnProperty("message")) {

                    errorsContainer.innerHTML = null;

                    let span = document.createElement("span");

                    span.textContent = jsonResponse["message"];

                    span.style.color = "red";
                    span.style.display = "block";
                    span.style.textAlign = "center";

                    errorsContainer.appendChild(span);

                }
                else {

                    sessionStorage.setItem("username", jsonResponse["username"]);

                    sessionStorage.setItem("userId", jsonResponse["id"]);

                    window.location.href = "http://127.0.0.1:5500/NotesApp-FrontEnd/index.html";

                }

            } catch (error) {

                console.error(error);

            }

            clearForm();

        } else {

            errorsContainer.innerHTML = null;

            let errorSpan = document.createElement("span");

            errorSpan.textContent = "Passwords don't match!";

            errorSpan.style.color = "red";
            errorSpan.style.display = "block";
            errorSpan.style.textAlign = "center";

            errorsContainer.appendChild(errorSpan);

        }

    }

    function clearForm() {

        emailInput.value = null;
        usernameInput.value = null;
        passwordInput.value = null;
        passwordRepeatInput.value = null;

    }

}

mainFunction();