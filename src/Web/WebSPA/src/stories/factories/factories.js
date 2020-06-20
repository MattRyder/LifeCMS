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

const userProfileFactory = () => ({
    id: faker.random.uuid(),
    name: faker.name.findName(),
    email: faker.internet.exampleEmail(),
    headerImageUrl: faker.image.imageUrl(),
    avatarImageUrl: 'https://thispersondoesnotexist.com/image',
    occupation: faker.name.jobTitle(),
    location: faker.address.city(),
});

export const createPost = postFactory;
export const createUserProfile = userProfileFactory;
