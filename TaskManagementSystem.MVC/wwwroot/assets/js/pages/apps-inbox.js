/**
 * Theme: Attex - Responsive Tailwind CSS 3 Admin Dashboard
 * Author: Coderthemes
 * Module/App: Inbox 
 */



// Snow theme
var quill = new Quill('#snow-editor', {
    theme: 'snow',
    modules: {
        'toolbar': [['bold', 'italic', 'underline', 'strike'], [{ 'color': [] }], ['blockquote', 'code-block'], [{ 'list': 'ordered' }, { 'list': 'bullet' }], ['link', 'image', 'video']]
    },
});