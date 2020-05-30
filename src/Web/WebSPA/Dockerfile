FROM node:12.9-alpine AS react-build
WORKDIR /app
COPY ./src/Web/WebSPA /app

RUN yarn install
RUN yarn build

FROM nginx:1.17
EXPOSE 80
COPY --from=react-build /app/build /var/www/lifecms-frontend
COPY --from=react-build /app/.docker/generate-runtime-configuration.sh /var/www/lifecms-frontend
COPY --from=react-build /app/.docker/start.sh /var/www/lifecms-frontend
COPY --from=react-build /app/.docker/nginx.conf /etc/nginx/nginx.conf

RUN chmod +x /var/www/lifecms-frontend/generate-runtime-configuration.sh
RUN chmod +x /var/www/lifecms-frontend/start.sh

CMD ["/var/www/lifecms-frontend/start.sh"]