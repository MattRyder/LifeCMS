import * as lzutf8 from 'lzutf8';

const STATE_ITEM_KEY = 'applicationPersistedState';

const decompressState = (state) => {
    const stateString = lzutf8.decompress(
        state,
        { inputEncoding: 'Base64' },
    );

    return stateString ? JSON.parse(stateString) : {};
};

const compressState = (stateObject) => lzutf8.compress(
    JSON.stringify(stateObject),
    { outputEncoding: 'Base64' },
);

export function saveState(store) {
    localStorage.setItem(STATE_ITEM_KEY, compressState(store.getState()));
}

export function loadState() {
    const state = localStorage.getItem(STATE_ITEM_KEY);

    return state ? decompressState(state) : {};
}
