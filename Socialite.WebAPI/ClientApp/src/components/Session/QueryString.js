const QueryString = require('query-string');

export function getRedirectUriFromProps(props) {
    const { location: { search: search } } = props;

    if (search) {
        const { ReturnUrl } = QueryString.parse(search);

        return ReturnUrl;
    }
}