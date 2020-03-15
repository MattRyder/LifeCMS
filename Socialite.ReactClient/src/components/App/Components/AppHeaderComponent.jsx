import React, { Component } from 'react';
import { push } from 'connected-react-router';
import {
    Collapse,
    Container,
    Nav,
    Navbar,
    NavbarBrand,
    NavItem,
    NavLink,
    NavbarToggler,
} from 'reactstrap';
import './AppHeaderComponent.scss';
import { connect } from 'react-redux';
import userManager from '../../../openid/UserManager';
import { performLogout } from '../../../redux/actions/LogoutActions';
import SocialiteLogo from '../../../assets/images/socialite-logo.svg';

const mapStateToProps = ({ oidc }) => ({
    user: oidc.user,
});

const mapDispatchToProps = (dispatch) => ({
    dispatchPerformLogout: () => dispatch(performLogout()),
})

class AppHeaderComponent extends Component {
    constructor(props) {
        super(props);

        this.toggle = this.toggle.bind(this);

        this.onLogoutButtonClick = this.onLogoutButtonClick.bind(this);

        this.state = {
            isOpen: false,
        };
    }

    static onLoginButtonClick() {
        userManager.signinRedirect();
    }

    onLogoutButtonClick() {
        const { dispatchPerformLogout } = this.props;

        dispatchPerformLogout();
    }

    toggle() {
        this.setState((prevState) => ({ isOpen: !prevState.isOpen }));
    }

    render() {
        const { isOpen } = this.state;

        const { user } = this.props;

        return (
            <Container fluid className="p-0">
                <Navbar className="navbar-socialite" expand="md">
                    <NavbarBrand href="/">
                        <img src={SocialiteLogo} alt="socialite logo" />
                    </NavbarBrand>

                    <NavbarToggler onClick={this.toggle} />

                    <Collapse isOpen={isOpen} navbar>
                        <Nav className="col-md-9" navbar>
                            <NavItem>
                                <NavLink href="/profile/1">Profile</NavLink>
                            </NavItem>
                            <NavItem>
                                {!user || user.expired
                                    ? (
                                        <NavLink href="#" onClick={AppHeaderComponent.onLoginButtonClick}>Login</NavLink>
                                    )
                                    : (
                                        <NavLink href="#" onClick={this.onLogoutButtonClick}>Logout</NavLink>
                                    )}
                            </NavItem>
                        </Nav>
                    </Collapse>
                </Navbar>
            </Container>
        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(AppHeaderComponent);
