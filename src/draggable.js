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

    node.addEventListener('mousedown', startDrag, true);
    node.addEventListener('mousemove', drag, true);
    node.addEventListener('mouseup', endDrag, true);
    node.addEventListener('mouseleave', endDrag, true);
    node.addEventListener('touchstart', startDrag, true);
    node.addEventListener('touchmove', drag, true);
    node.addEventListener('touchend', endDrag, true);

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

        const cx = event.clientX || event.touches[0].clientX;
        const cy = event.clientY || event.touches[0].clientY;
        lastX = cx;
        lastY = cy;

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

        
        const cx = event.clientX || event.touches[0].clientX;
        const cy = event.clientY || event.touches[0].clientY;
        const dx = cx - lastX;
        const dy = cy - lastY;        
        lastX = cx;
        lastY = cy;

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

        isDragging = false;
    }

    function setOffsets(x,y)
    {
        offsetX = x;
        offsetY = y;
        node.style.left = offsetX + 'px';
        node.style.top = offsetY + 'px';
    }
    
    return {
        destroy() {
            node.removeEventListener('mousedown', startDrag, true);
            node.removeEventListener('mousemove', drag, true);
            node.removeEventListener('mouseup', endDrag, true);
            node.removeEventListener('mouseleave', endDrag, true);
            console.log("cleaned up event handlers")
        }
    }
}