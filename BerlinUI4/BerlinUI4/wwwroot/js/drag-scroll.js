function enableDragScroll(id) {
    const el = document.getElementById(id);
    let isDown = false;
    let  startY;
    let  scrollTop;

    el.addEventListener('mousedown', e => {
        isDown = true;
        startY = e.pageY - el.offsetTop;
        scrollTop = el.scrollTop;
    });

    el.addEventListener('mouseleave', () => {
        isDown = false;
    });

    el.addEventListener('mouseup', () => {
        isDown = false;
    });

    el.addEventListener('mousemove', e => {
        if (!isDown) return;
        e.preventDefault();
        const y = e.pageY - el.offsetTop;
        const walkY = (y - startY);
        el.scrollTop = scrollTop - walkY;
    });
}

function enableDragScrollL(elementId) {
    const el = document.getElementById(elementId);
    if (!el) {
        console.error(`Element with ID ${elementId} not found.`);
        return;
    }

    let isDown = false;
    let startX;
    let scrollLeft;

    el.addEventListener('mousedown', e => {
        isDown = true;
        el.classList.add('active'); // Opțional, pentru efecte vizuale când se face drag
        startX = e.pageX - el.offsetLeft;
        scrollLeft = el.scrollLeft;
        e.preventDefault(); // Previne selectarea textului sau alte acțiuni default
    });

    el.addEventListener('mouseleave', () => {
        isDown = false;
        el.classList.remove('active'); // Opțional, pentru efecte vizuale
    });

    el.addEventListener('mouseup', () => {
        isDown = false;
        el.classList.remove('active'); // Opțional, pentru efecte vizuale
    });

    el.addEventListener('mousemove', e => {
        if (!isDown) return;
        e.preventDefault();
        const x = e.pageX - el.offsetLeft;
        const walk = (x - startX) * 3; // Factorul *3 mărește viteza de scroll, ajustează după necesități
        el.scrollLeft = scrollLeft - walk;
    });
}
