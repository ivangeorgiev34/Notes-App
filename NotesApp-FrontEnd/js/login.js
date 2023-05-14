function mainFunction() {

    let emailInput = document.querySelector("#email");
    let passwordInput = document.querySelector("#password");

    emailInput.addEventListener("focus", changeBoxShadow);
    passwordInput.addEventListener("focus", changeBoxShadow);

    emailInput.addEventListener("blur", clearBoxShadow);
    passwordInput.addEventListener("blur", clearBoxShadow);

    let submitBtn = document.querySelector(".btn-submit");

    submitBtn.addEventListener("mouseover", changeBoxShadow);
    submitBtn.addEventListener("mouseout", clearBoxShadow);
    submitBtn.addEventListener("click", submitForm)

    let errorsContainer = document.querySelector(".errors-container");

    function changeBoxShadow(event) {

        event.target.style.boxShadow = "0px 0px 5px 0px #000000";

    }

    function clearBoxShadow(event) {

        event.target.style.boxShadow = null;

    }

    async function submitForm(event) {

        event.preventDefault();

        let email = emailInput.value;
        let password = passwordInput.value;

        let user = {
            email: email,
            password: password
        };

        try {

            let postMethod = {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(user)
            };

            let submitFormResponse = await fetch("https://localhost:7076/api/User/login", postMethod);

            let jsonResponse = await submitFormResponse.json();


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

                window.location.href = "http://127.0.0.1:5500/NotesApp-FrontEnd/index.html";

            }

        } catch (error) {

            console.error(error.message);

        }

        clearForm();
    }

    function clearForm() {

        emailInput.value = null;
        passwordInput.value = null;

    }

}

mainFunction();