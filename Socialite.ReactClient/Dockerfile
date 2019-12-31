FROM node:12.9-alpine AS react-build
WORKDIR /app
COPY ./ /app
RUN yarn install
RUN yarn build

FROM nginx:1.17
COPY --from=react-build /app/build /var/www/socialite-frontend
COPY ./.docker/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]