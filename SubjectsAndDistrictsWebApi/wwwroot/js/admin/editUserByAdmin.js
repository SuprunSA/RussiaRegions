import { render, contains } from "../renderHeader.js";
import { apiMethods } from "../requests.js";

window.addEventListener('load', () => {
    render();
    let form = document.getElementById('edit-user-form');
    let path = 'user/' + new URLSearchParams(location.search).get('userName');
    apiMethods.get(path)
        .then((result) => {
            form[0].value = result.userName;
            form[1].value = result.email;
            form[2].value = result.firstName;
            form[3].value = result.middleName;
            form[4].value = result.lastName;
        })
        .catch(error => console.log(error));

    form.addEventListener('submit', (evt) => {
        evt.preventDefault();

        let role = form[5].value;
        if (role !== 'none') {
            apiMethods.post('user/role/' + role + '/' + form[0].value)
                .then(() => console.log('успешно добавлена роль'))
                .catch(error => console.warn(error));
        }

        apiMethods.put('user', {
                userName: form[0].value,
                email: form[1].value,
                firstName: form[2].value,
                middleName: form[3].value,
                lastName: form[4].value
            })
            .then(() => {
                location.pathname = 'html/adminOpp.html';
                console.log('успешно обновлён');
            })
            .catch(error => console.log(error));
    });
});