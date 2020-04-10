import { UserProfile } from '../../components/Profile/BasicInfoComponent';
import Status from '../../components/Statuses/Status';

const faker = require('faker');

const generateText = (paragraphs = 5) => Array.from(
    Array(paragraphs),
    () => `${faker.lorem.paragraph(10)}\n`,
);

const postFactory = () => ({
    id: faker.random.uuid(),
    title: faker.lorem.sentence(),
    text: generateText(),
    createdAt: faker.date.past(1),
});

const statusFactory = () => {
    return new Status(
        faker.random.arrayElement(['😎', '👌', '💯']),
        faker.lorem.sentences(5),
        faker.date.past(2),
    );
};

const userFactory = () => {
    return new UserProfile(
        faker.name.firstName(),
        faker.name.lastName(),
        faker.name.jobTitle(),
        faker.company.companyName(),
        "https://thispersondoesnotexist.com/image");
};

export const createPost = postFactory;
export const createStatus = statusFactory;
export const createUser = userFactory;