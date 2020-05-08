FROM node:12.9-alpine AS react-build
WORKDIR /app
COPY ./Socialite.ReactClient /app

RUN yarn install
RUN yarn build

FROM nginx:1.17
COPY --from=react-build /app/build /var/www/socialite-frontend
COPY --from=react-build /app/.docker/generate-runtime-configuration.sh /var/www/socialite-frontend
COPY --from=react-build /app/.docker/start.sh /var/www/socialite-frontend
COPY --from=react-build /app/.docker/nginx.conf /etc/nginx/nginx.conf

RUN chmod +x /var/www/socialite-frontend/generate-runtime-configuration.sh
RUN chmod +x /var/www/socialite-frontend/start.sh

EXPOSE 80

CMD ["/var/www/socialite-frontend/start.sh"]