FROM node:12.9-alpine AS react-build
WORKDIR /app
COPY ./Socialite.ReactClient /app

RUN yarn install
RUN yarn build

FROM nginx:1.17
COPY --from=react-build /app/build /var/www/socialite-frontend
COPY --from=react-build /app/.docker/generate-runtime-configuration.sh /var/www/socialite-frontend
COPY --from=react-build /app/.docker/nginx.conf /etc/nginx/nginx.conf

RUN /var/www/socialite-frontend/generate-runtime-configuration.sh /var/www/socialite-frontend/runtime/config.js

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]