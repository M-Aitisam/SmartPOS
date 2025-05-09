export function initializeSidebar(dotNetHelper) {
    // Initialize all Bootstrap collapses
    const collapses = document.querySelectorAll('.collapse');
    collapses.forEach(collapse => {
        new bootstrap.Collapse(collapse, {
            toggle: false
        });
    });

    // Handle sidebar link clicks
    const sidebarLinks = document.querySelectorAll('.sidebar-link[data-bs-toggle="collapse"]');
    sidebarLinks.forEach(link => {
        link.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('data-bs-target'));
            bootstrap.Collapse.getOrCreateInstance(target).toggle();

            // Toggle the collapsed class
            this.classList.toggle('collapsed');
        });
    });
}