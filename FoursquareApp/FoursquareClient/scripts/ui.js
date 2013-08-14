var ui = (function () {

    function buildLoginForm() {
        var html =
            '<div id="login-form-holder">' +
				'<form>' +
					'<div id="login-form">' +
						'<label for="tb-login-username">Username: </label>' +
						'<input type="text" id="tb-login-username"><br />' +
						'<label for="tb-login-password">Password: </label>' +
						'<input type="text" id="tb-login-password"><br />' +
						'<button id="btn-login" class="button">Login</button>' +
					'</div>' +
					'<div id="register-form" style="display: none">' +
						'<label for="tb-register-username">Username: </label>' +
						'<input type="text" id="tb-register-username"><br />' +
						'<label for="tb-register-password">Password: </label>' +
						'<input type="text" id="tb-register-password"><br />' +
						'<button id="btn-register" class="button">Register</button>' +
					'</div>' +
					'<a href="#" id="btn-show-login" class="button selected">Login</a>' +
					'<a href="#" id="btn-show-register" class="button">Register</a>' +
				'</form>' +
				'<div id="error-messages"></div>' +
            '</div>';
        return html;
    }

    function LoadHomeWhenLogged()
    {
        var html =
            '<div id="main">' +
                 '<div id="leftMenu">' +
                 'lovech, sofia, varna lovech, sofia, varna'+
                 //print all places
                 '</div>' +
                 '<div id="center">' +
                 'some central text'+
                 //pictures and other content
                 '</div>' +
                 '<div id="nearPlaces">' +
                 'lovech, sofia, varna'+
                 '</div>'+
            '</div>';

        return html;
    }

    function buildMessagesList(messages) {
        var list = '<ul class="messages-list">';
        var msg;
        for (var i = 0; i < messages.length; i += 1) {
            msg = messages[i];
            var item =
				'<li>' +
					'<a href="#" class="message-state-' + msg.state + '">' +
						msg.text +
					'</a>' +
				'</li>';
            list += item;
        }
        list += '</ul>';
        return list;
    }

    return {
        loginForm: buildLoginForm,
        messagesList: buildMessagesList,
        LoadHomeWhenLogged: LoadHomeWhenLogged
    }

}());