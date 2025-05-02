
        // Store sidebar state in localStorage
    const sidebarState = localStorage.getItem('sidebarState');
    const bodyPd = document.getElementById('body-pd');
    const headerPd = document.getElementById('header');
    const navBar = document.getElementById('nav-bar');
    const headerToggle = document.getElementById('header-toggle');

    document.addEventListener("DOMContentLoaded", function(event) {
            // Initialize sidebar state
            if (sidebarState === 'open') {
        navBar.classList.add('show');
    headerToggle.classList.add('bx-x');
    bodyPd.classList.add('body-pd');
    headerPd.classList.add('body-pd');
            }

            // Toggle sidebar
            headerToggle.addEventListener('click', () => {
                const isOpen = navBar.classList.contains('show');
    navBar.classList.toggle('show');
    headerToggle.classList.toggle('bx-x');
    bodyPd.classList.toggle('body-pd');
    headerPd.classList.toggle('body-pd');

    // Store state
    localStorage.setItem('sidebarState', isOpen ? 'closed' : 'open');
            });

    // Active link functionality
    const linkColor = document.querySelectorAll('.nav_link:not(.nav_logo)');

    function colorLink() {
                if (linkColor) {
        linkColor.forEach(l => l.classList.remove('active'));
    this.classList.add('active');
                }
            }

            linkColor.forEach(l => l.addEventListener('click', function(e) {
                // Don't run colorLink if clicking on submenu arrow
                if (!e.target.classList.contains('submenu-arrow')) {
        colorLink.call(this);
                }
            }));
        });

    function toggleSubmenu(event, submenuId) {
        event.preventDefault();
    const submenu = document.getElementById(submenuId);
    const arrow = event.currentTarget.querySelector('.submenu-arrow');

            // Close all other submenus first
            document.querySelectorAll('.submenu').forEach(menu => {
                if (menu.id !== submenuId && menu.classList.contains('show-submenu')) {
        menu.classList.remove('show-submenu');
    const otherArrow = menu.parentElement.querySelector('.submenu-arrow');
    if (otherArrow) {
        otherArrow.classList.remove('bx-chevron-up');
    otherArrow.classList.add('bx-chevron-down');
                    }
                }
            });

    // Toggle current submenu
    submenu.classList.toggle('show-submenu');

    // Toggle arrow icon
    if (submenu.classList.contains('show-submenu')) {
        arrow.classList.remove('bx-chevron-down');
    arrow.classList.add('bx-chevron-up');
            } else {
        arrow.classList.remove('bx-chevron-up');
    arrow.classList.add('bx-chevron-down');
            }
        }
