const QueryString = require('query-string');

export function getParamFromSearch(props, paramName) {
    const { location: { search: search } } = props;

    return QueryString.parse(search)[paramName];
}

export function createUrlWithQueryString(baseUrl, params) {
    const queryString = QueryString.stringify(params, { skipNull: true });

    return queryString ? `${baseUrl}?${queryString}` : baseUrl;
}