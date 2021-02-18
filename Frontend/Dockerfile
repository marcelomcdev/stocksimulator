FROM nginx:1.19.7-alpine

RUN apk update && \
    apk add nodejs && \
    apk add nodejs-npm && \
    mkdir /app && \
    npm install -g @angular/cli
WORKDIR /app
COPY ./ .
RUN npm i
EXPOSE 4200
CMD ["npm", "start"]
