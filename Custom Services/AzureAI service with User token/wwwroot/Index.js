async function fingerPrint() {
    try {
        var canvas = document.body.appendChild(document.createElement('canvas'));
        canvas.width = 600;
        canvas.height = 300;
        canvas.style.display = "none";
        const ctx = canvas.getContext("2d");
        const size = 24;
        const diamondSize = 28;
        const gap = 4;
        const startX = 30;
        const startY = 30;
        const blue = "#1A3276";
        const orange = "#F28C00";
        const colorMap = [
            ["blue", "blue", "diamond"],
            ["blue", "orange", "blue"],
            ["blue", "blue", "blue"]
        ];
        function drawSquare(x, y, color) {
            ctx.fillStyle = color;
            ctx.fillRect(x, y, size, size);
        }
        function drawDiamond(centerX, centerY, size, color) {
            ctx.fillStyle = color;
            ctx.beginPath();
            ctx.moveTo(centerX, centerY - size / 2);
            ctx.lineTo(centerX + size / 2, centerY);
            ctx.lineTo(centerX, centerY + size / 2);
            ctx.lineTo(centerX - size / 2, centerY);
            ctx.closePath();
            ctx.fill();
        }
        for (let row = 0; row < 3; row++) {
            for (let col = 0; col < 3; col++) {
                const type = colorMap[row][col];
                const x = startX + col * (size + gap);
                const y = startY + row * (size + gap);
                if (type === "blue") drawSquare(x, y, blue);
                else if (type === "orange") drawSquare(x, y, orange);
                else if (type === "diamond") drawDiamond(x + size / 2, y + size / 2, diamondSize, orange);
            }
        }
        ctx.font = "20px Arial";
        ctx.fillStyle = blue;
        ctx.textBaseline = "middle";
        ctx.fillText("Syncfusion", startX + 3 * (size + gap) + 20, startY + size + gap);
        ctx.globalCompositeOperation = "multiply";
        ctx.fillStyle = "rgb(255,0,255)";
        ctx.beginPath(); ctx.arc(50, 200, 50, 0, Math.PI * 2); ctx.fill();
        ctx.fillStyle = "rgb(0,255,255)";
        ctx.beginPath(); ctx.arc(100, 200, 50, 0, Math.PI * 2); ctx.fill();
        ctx.fillStyle = "rgb(255,255,0)";
        ctx.beginPath(); ctx.arc(75, 250, 50, 0, Math.PI * 2); ctx.fill();
        ctx.fillStyle = "rgb(255,0,255)";
        ctx.beginPath();
        ctx.arc(200, 200, 75, 0, Math.PI * 2, true);
        ctx.arc(200, 200, 25, 0, Math.PI * 2, true);
        ctx.fill("evenodd");
        const sha256 = async function (str) {
            const encoder = new TextEncoder();
            const data = encoder.encode(str);
            const hashBuffer = await crypto.subtle.digest('SHA-256', data);
            const hashArray = Array.from(new Uint8Array(hashBuffer));
            return hashArray.map(b => b.toString(16).padStart(2, '0')).join('');
        };

        const visitorID = sha256(canvas.toDataURL());
        return visitorID;
    }
    catch (error) {
        console.error(error);
        return null;
    }
}

function showBanner(messageText) {
    // Check if the banner already exists
    if (document.getElementById("custom-banner")) {
        hideSpinner();
        return;
    }

    // Create the banner container
    let banner = document.createElement("div");
    banner.id = "custom-banner";
    banner.className = "e-banner";

    // Banner content
    let message = document.createElement("p");
    message.innerHTML = messageText;
    message.className = "banner-message";

    // Create the close button
    let closeButton = document.createElement("span");
    closeButton.innerHTML = "&times;"; // HTML entity for '×' symbol
    closeButton.className = "close-button";
    closeButton.onclick = closeBanner;

    // Append elements
    banner.appendChild(message);
    banner.appendChild(closeButton);
    document.body.insertBefore(banner, document.body.firstChild);
    hideSpinner();
}

async function getRemainingTokens(userId) {
    try {
        const baseElement = document.querySelector('base');
        const baseUrl = baseElement ? baseElement.href : window.location.origin;
        const response = await fetch(`${baseUrl}api/UserTokens/get_remaining_tokens/${userId}`);
        if (response.ok) {
            return await response.json();
        }
    } catch (error) {
        console.error("Error fetching remaining tokens:", error);
    }
    return 0;
}
function closeBanner() {
    let banner = document.getElementById("custom-banner");
    if (banner) {
        document.body.removeChild(banner);
    }
}

function hideSpinner() {
    var spinnerElement = document.querySelector('.e-spinner-pane.e-spin-show');
    if (spinnerElement) {
        spinnerElement.classList.remove('e-spin-show');
        spinnerElement.classList.add('e-spin-hide');
    }
}
