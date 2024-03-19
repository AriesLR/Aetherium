window.scrollToBottom = () => {
    const textarea = document.getElementById('outputTextarea');
    textarea.scrollTop = textarea.scrollHeight;
};
