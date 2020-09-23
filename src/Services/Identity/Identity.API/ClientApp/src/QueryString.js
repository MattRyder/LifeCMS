const QueryString = require('query-string');

export function getParamFromSearch(searchString, paramName) {
    return QueryString.parse(searchString)[paramName];
}

export function createUrlWithQueryString(baseUrl, params) {
    const queryString = QueryString.stringify(params, { skipNull: true });

    return queryString ? `${baseUrl}?${queryString}` : baseUrl;
}

export function makeReturnHref(search, baseHref) {
    const returnUrl = getParamFromSearch(
        search,
        'ReturnUrl',
    );

    return createUrlWithQueryString(
        baseHref,
        { ReturnUrl: returnUrl },
    );
}
