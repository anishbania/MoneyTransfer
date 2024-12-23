let currentExchangeRate = null;
let lastRateFetch = null;
const RATE_CACHE_DURATION = 5 * 60 * 1000; // 5 minutes

async function getExchangeRate() {
    if (currentExchangeRate && lastRateFetch &&
        (Date.now() - lastRateFetch < RATE_CACHE_DURATION)) {
        return currentExchangeRate;
    }

    try {
        const response = await fetch('/ForexService/GetMYRRate');
        if (!response.ok) {
            throw new Error('Failed to fetch exchange rate');
        }
        const data = await response.json();

        currentExchangeRate = data.rate;
        lastRateFetch = Date.now();

        return currentExchangeRate;
    } catch (error) {
        console.error('Error fetching exchange rate:', error);
        return currentExchangeRate || 28.93;
    }
}

function calculateConversion(amount, rate) {
    return amount * rate;
}

function debounce(func, wait) {
    let timeout;
    return function (...args) {
        clearTimeout(timeout);
        timeout = setTimeout(() => func.apply(this, args), wait);
    };
}

function formatNumber(number) {
    return number.toLocaleString('en-US', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    });
}

async function convertToNepaliCurrency() {
    const youSendInput = document.getElementById('youSend');
    const recipientGetsInput = document.getElementById('recipientGets');

    const myrAmount = parseFloat(youSendInput.value.replace(/,/g, '')) || 0;
    const exchangeRate = await getExchangeRate();

    const nprAmount = calculateConversion(myrAmount, exchangeRate);

    recipientGetsInput.value = formatNumber(nprAmount);
    document.getElementById('currentRate').textContent = formatNumber(exchangeRate);
}

const debouncedConversion = debounce(convertToNepaliCurrency, 300);

document.getElementById('youSend').addEventListener('input', (e) => {
    if (currentExchangeRate) {
        const amount = parseFloat(e.target.value.replace(/,/g, '')) || 0;
        const roughConversion = calculateConversion(amount, currentExchangeRate);
        document.getElementById('recipientGets').value = formatNumber(roughConversion);
    }
    debouncedConversion();
});

document.addEventListener('DOMContentLoaded', async () => {
    await getExchangeRate();
    convertToNepaliCurrency();

    setInterval(async () => {
        await getExchangeRate();
        convertToNepaliCurrency();
    }, RATE_CACHE_DURATION);
});
// Function to update the displayed rate
function updateDisplayedRate(rate) {
    const currentRate = document.getElementById('currentRate');
    if (currentRate) {
        currentRate.textContent = rate.toLocaleString('en-US', {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
        });
    }
}

// Update your getExchangeRate function to call this
async function getExchangeRate() {
    try {
        const response = await fetch('/ForexService/GetMYRRate');
        if (!response.ok) {
            throw new Error('Failed to fetch exchange rate');
        }
        const data = await response.json();
        currentExchangeRate = data.rate;
        updateDisplayedRate(currentExchangeRate); // Update the displayed rate
        return currentExchangeRate;
    } catch (error) {
        console.error('Error fetching exchange rate:', error);
        return currentExchangeRate || 28.93;
    }
}

