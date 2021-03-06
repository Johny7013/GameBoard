﻿class Collapsible {
    private readonly sidebarContainer: HTMLElement;
    private readonly sidebar: HTMLElement;
    private readonly sidebarCollapse: HTMLButtonElement;

    constructor(sidebarContainer: HTMLElement,
        sidebar: HTMLElement,
        sidebarCollapse: HTMLButtonElement) {
        this.sidebarContainer = sidebarContainer;
        this.sidebar = sidebar;
        this.sidebarCollapse = sidebarCollapse;

        this.sidebarCollapse.addEventListener("click",
            () => {
                this.sidebarContainer.classList.toggle("toggled");
                this.toggleSidebarCollapseIcon();
            });
    }

    toggleSidebarCollapseIcon() {
        const icon = this.sidebarCollapse.querySelector("i");

        if (!icon) {
            console.error("Cannot find icon for toggleSidebarCollapseIcon");
            return;
        }

        if (icon.classList.contains("fa-angle-left")) {
            icon.classList.remove("fa-angle-left");
            icon.classList.add("fa-angle-right");
        } else {
            icon.classList.remove("fa-angle-right");
            icon.classList.add("fa-angle-left");
        }
    }
}