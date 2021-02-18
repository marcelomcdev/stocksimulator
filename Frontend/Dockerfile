FROM alpine:3.7

RUN apk update && \
    apk add nodejs && \
    apk add nodejs-npm && \
    mkdir /app && \
    npm install express -g
WORKDIR /src/app
COPY ./ .
RUN chmod +x start.sh && \
    npm i --only=prod
EXPOSE 5000
CMD ["npm", "start"]
