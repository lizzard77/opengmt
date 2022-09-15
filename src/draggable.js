function captureClick(e) 
{
    e.stopPropagation(); // Stop the click from being propagated.
    window.removeEventListener('click', captureClick, true); // cleanup
}

export function draggable(node, params) {
    let lastX;
    let lastY;
    let contentRect;
    let offsetX = 0;
    let offsetY = 0;
    let isDragging = false;
    let containerWidth = 0;
    let containerHeight = 0;
    let moved = false;
    let maxX = 0;
    let maxY = 0;

    node.style.position = "absolute";
    node.style.left = offsetX + 'px';
    node.style.top = offsetY + 'px';

    node.addEventListener('mousedown', startDrag);
    node.addEventListener('mousemove', drag);
    node.addEventListener('mouseup', endDrag);
    node.addEventListener('mouseleave', endDrag);

    new ResizeObserver(() => {
        offsetX = 0;
        offsetY = 0;
        node.style.left = offsetX + 'px';
        node.style.top = offsetY + 'px';
    }).observe(node);

    function startDrag(event) 
    {
        event.preventDefault();
        isDragging = true;
        moved = false;
        lastX = event.clientX;
        lastY = event.clientY;

        contentRect = node.getBoundingClientRect();
        containerWidth = node.parentNode.clientWidth;
        containerHeight = node.parentNode.clientHeight;
        maxX = -(contentRect.width - containerWidth);
        maxY = -(contentRect.height - containerHeight);
    }

    function drag(event) 
    {
        if (!isDragging)
            return;
        event.preventDefault();

        const dx = event.clientX - lastX;
        const dy = event.clientY - lastY;
        lastX = event.clientX;
        lastY = event.clientY;

        if (dx || dy)
            moved = true;
        offsetX += dx;
        offsetY += dy;
        
        if (offsetX < maxX && maxX < 0) offsetX = maxX;
        if (offsetY < maxY && maxY < 0) offsetY = maxY;
        if (offsetX > 0 || maxX > 0) offsetX = 0;
        if (offsetY > 0 || maxY > 0) offsetY = 0;
        
        node.style.left = offsetX + 'px';
        node.style.top = offsetY + 'px';
    }

    function endDrag(event) 
    {
        if (!isDragging)
            return;
        event.preventDefault();

        if (moved)
            window.addEventListener('click', captureClick, true);  // <-- This registeres this listener for the capture phase instead of the bubbling phase!

        lastX = event.clientX;
        lastY = event.clientY;
        isDragging = false;
    }
}