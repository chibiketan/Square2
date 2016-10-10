FROM nickstenning/slapd
# RUN mkdir -p /docker-entrypoint-initdb.d
ADD ./ldif /mnt/ldif/

RUN apt-get update \
    && apt-get install -y ldap-utils \
    && rm -rf /var/lib/apt/lists/* \
    && ldapadd -v -h localhost:389 -c -x -D cn=admin,dc=funeco,dc=com -w $LDAP_ROOTPASS -f /mnt/ldif/user.ldif
